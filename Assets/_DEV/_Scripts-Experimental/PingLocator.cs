using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingLocator : MonoBehaviour
{
    [SerializeField] private int _pingRadius;
    [SerializeField] private float _waitDelay = 5f;

    private Transform _player;

    bool started = false;

    private ParticleSystem _particles;

    public delegate void OnPingFinished(List<Pingable> pings);
    public static OnPingFinished onPingFinished;

    private List<Pingable> pings = new List<Pingable>();
    
    void OnEnable(){
        _particles = GetComponent<ParticleSystem>();

    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && !started){
            StartCoroutine(StartPing());
        }
    }

    private IEnumerator StartPing(){
        pings.Clear();
        transform.position = _player.position;
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, _pingRadius);
        foreach(Collider2D coll in colls){
            if(coll.TryGetComponent<Pingable>(out Pingable ping)){
                Debug.Log("PINGED! " + ping.ReturnCurrentPos());
                pings.Add(ping);
            }
        }
        started = true;
        _particles.Emit(1);
        yield return new WaitForSeconds(_waitDelay);
        onPingFinished?.Invoke(pings);
        Debug.Log(pings);
        started = false;
    }
}
