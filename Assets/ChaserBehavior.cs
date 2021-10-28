using Assets.Scripts;
using Assets.Scripts.WarpSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ChaserBehavior : Moveable {
    public float ChaseRadius;
    public float SightRadius;
    public float SightConeAngle;
    public float HiddenDetectionRadius;
    public float HiddenSurveyRadius;

    private NavMeshAgent _agent;
    private WalkspriteAnimator _walksprite;
    
    private Transform _player;

    private Vector2 _move;
    public bool _scouting;
    private Vector3 _scoutTarget;
    public bool _chasing;
    private Collider2D _nextRoom;
    private Action _onDestinationReached;
    private bool _isTravelling;

    private Queue<Vector2> _patrolRoute;
    private Vector2 _nextPatrolDest;

    // Start is called before the first frame update
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updatePosition = true;
        _agent.updateUpAxis = false;
        
        AssignRigidbody2D(GetComponent<Rigidbody2D>());

        _walksprite = GetComponent<WalkspriteAnimator>();

        _player = GameObject.FindWithTag("Player").transform;

        _patrolRoute = new Queue<Vector2>();
        _patrolRoute.Enqueue(new Vector2(113f, 80f)); //Middle of BNG
        _patrolRoute.Enqueue(new Vector2(48f, -3f)); //Middle of T1G
        _patrolRoute.Enqueue(new Vector2(117f, 9f)); //Middle of FOUNG
        _patrolRoute.Enqueue(new Vector2(113f, 80f)); //Middle of BNG
        _patrolRoute.Enqueue(new Vector2(175f, -1f)); //Middle of T2G
        _patrolRoute.Enqueue(new Vector2(117f, 9f)); //Middle of FOUNG
    }

    // Update is called once per frame
    protected void Update() {

        if (_agent.isOnOffMeshLink && !_isTravelling)
        {
            StartCoroutine(Travel());
        }

        Collider2D playerRoom = Camera.main.GetComponent<PlayerCamera>().CurrentArea;
        Vector3 zAdjustedPosition = new Vector3(transform.position.x, transform.position.y, 0);

        if (_agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance <= 0)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(_scoutTarget, out hit, 0.5f, NavMesh.GetAreaFromName("Walkable"));
            if (Vector3.Distance(zAdjustedPosition, _scoutTarget) <= 1f || !hit.hit)
            {
                _scoutTarget = Vector3.zero;
                _scouting = false;
            }

            if (_onDestinationReached != null)
            {
                _onDestinationReached.Invoke();
                _onDestinationReached = null;
            }
            else if (!_chasing && !_scouting)
            {
                Vector3 nextDest = _patrolRoute.Dequeue();
                nextDest.z = transform.position.z;
                _nextPatrolDest = nextDest;
                _patrolRoute.Enqueue(nextDest);
            }
        }

        if (!_isTravelling) {
            CheckPlayer();
        }

        if (_agent.pathStatus == NavMeshPathStatus.PathPartial && !_agent.hasPath)
        {
            Vector3 targetPos = !_chasing && _scouting ? _scoutTarget : _player.position;
            RaycastHit2D hit = Physics2D.Raycast(zAdjustedPosition, targetPos - zAdjustedPosition);
            _agent.SetDestination(hit.point);
        }

        if (_chasing)
        {
            Chase();
        } else if (_scouting)
        {
            Scout();
        } else
        {
            Patrol();
        }

        if (!_scouting && _scoutTarget != Vector3.zero)
        {
            _scoutTarget = Vector3.zero;
        }
    }

    protected override void FixedUpdate() {
    }

    void Chase() {
        _agent.SetDestination(_player.position);
        OnWalk?.Invoke(_agent.velocity);
    }

    void Scout()
    {
        _agent.SetDestination(_scoutTarget);
        OnWalk?.Invoke(_agent.velocity);
    }

    void Patrol()
    {
        _agent.SetDestination(_nextPatrolDest);
        OnWalk?.Invoke(_agent.velocity);
    }

    void CheckPlayer()
    {
        Vector3 direction = _walksprite.Facing;
        Vector3 zAdjustedPosition = new Vector3(transform.position.x, transform.position.y, 0);
        bool hidden = _player.GetComponent<Player>().Hidden;

        //Debug.DrawRay(transform.position, (_player.position - transform.position).normalized * HiddenSurveyRadius, Color.green);
        //Debug.DrawRay(transform.position, (_player.position - transform.position).normalized * SightRadius, Color.blue);
        //Debug.DrawRay(transform.position, (_player.position - transform.position).normalized * HiddenDetectionRadius, Color.red);
        //Debug.DrawRay(transform.position, (_player.position - transform.position) * ChaseRadius, Color.red);

        float distanceToPlayer = Vector3.Distance(zAdjustedPosition, _player.position);
        RaycastHit2D lineOfSightCast = Physics2D.Raycast(zAdjustedPosition, (_player.position - zAdjustedPosition).normalized, SightRadius, LayerMask.GetMask("Player"));
        RaycastHit2D wallCast = Physics2D.Raycast(zAdjustedPosition, (_player.position - zAdjustedPosition).normalized, SightRadius, LayerMask.GetMask("Walls"));

        bool hasLineOfSight;
        if (lineOfSightCast)
        {
            //Debug.DrawRay(zAdjustedPosition, Vector3.Scale(direction, (_player.position - zAdjustedPosition)), Color.red);
            //Debug.DrawRay(zAdjustedPosition, (zAdjustedPosition - (Vector3)lineOfSightCast.point), Color.blue);
            float angle = Vector2.Angle(Vector3.Scale(direction, (_player.position - zAdjustedPosition)), (zAdjustedPosition - (Vector3)lineOfSightCast.point));
            Debug.Log(angle);
            hasLineOfSight = (angle < SightConeAngle/2) && !wallCast;
        } else
        {
            hasLineOfSight = false;
        }

        if (hidden && _chasing && !_scouting)
        {
            _chasing = false;
            _scouting = true;

            if (distanceToPlayer < HiddenDetectionRadius || hasLineOfSight)
            {
                ApproachHideable(true);
            }
            else if (distanceToPlayer < HiddenSurveyRadius)
            {
                ApproachHideable(false);
            }
            else
            {
                _scouting = false;
            }
        }
        else if (!hidden && !wallCast && (distanceToPlayer < ChaseRadius || (distanceToPlayer < SightRadius) && hasLineOfSight || distanceToPlayer < SightRadius && _chasing == true))
        {
            _scouting = false;
            _chasing = true;
        }
        else if (_scoutTarget == Vector3.zero)
        {
            Collider2D playerRoom = Camera.main.GetComponent<PlayerCamera>().CurrentArea;
            if (!playerRoom.bounds.Contains(zAdjustedPosition) && _chasing == true)
            {
                _chasing = false;
                _scoutTarget = _player.position;
                _scouting = true;
            } else
            {
                _chasing = false;
                _scouting = false;
            }
        }
    }

    private void ApproachHideable(bool open)
    {
        HideableObject playerHideable = null;
        foreach (HideableObject hideable in FindObjectsOfType<MonoBehaviour>().OfType<HideableObject>())
        {
            if (hideable.Occupied == true)
            {
                playerHideable = hideable;
            }
        }

        _scoutTarget = new Vector3(playerHideable.transform.position.x, playerHideable.GetComponent<Collider2D>().bounds.min.y - 0.5f, 0);

        if (open)
        {
            _onDestinationReached = new Action(() => ForceOpenHideable(playerHideable));
        } else
        {
            _onDestinationReached = null;
        }
    }

    private void ForceOpenHideable(HideableObject hideable)
    {
        hideable.ForceOpen();
    }

    private Vector2 ConstrainTo8Dir(Vector2 velocity) {
        Vector2 res = velocity.normalized;

        float x;
        float y;

        if (res.x > 0.33f) x = 1f;
        else if (res.x > -0.33f) x = 0f;
        else x = -1f;
        
        if (res.y > 0.33f) y = 1f;
        else if (res.y > -0.33f) y = 0f;
        else y = -1f;
        
        return new Vector2(x, y).normalized;
    }

    private IEnumerator Travel()
    {
        _isTravelling = true;
           SpriteRenderer spRender = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<BoxCollider2D>();
        spRender.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(2f);
        _agent.CompleteOffMeshLink();
        spRender.enabled = true;
        collider.enabled = true;
        _isTravelling = false;
    }
}
