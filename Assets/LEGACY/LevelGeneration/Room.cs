using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Room : MonoBehaviour
{
    public enum Directions {
        up,
        right,
        down,
        left
    }

    [System.Serializable]
    public struct Doors
    {
        // [HideInInspector]
        public bool active;


        public Directions direction;
        public SpriteRenderer spriteR;
        public Room leadsTo;
    }

    [SerializeField]
    private SpriteRenderer body;

    public int amountOfChests;
    public Doors[] roomDoors = new Doors[4];


    public bool collision;

    void Start(){
        if(!GetComponent<LevelGenerator>()){
            body.color = new Color(Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f));
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        collision = true;
    }

    public void AssignNeighbors(Vector2[] offsets){

        for(int i = 0; i < roomDoors.Length; i++){
            Vector2 offset = offsets[(int)roomDoors[i].direction];
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, offset, LevelGenerator.prefabsDistance);

            for(int j = 0; j < hit.Length; j++){
                if(hit[j].collider != null && hit[j].collider.gameObject != gameObject){
                    roomDoors[i].leadsTo = hit[j].collider.GetComponentInChildren<Room>();
                    roomDoors[i].active = true;
                    roomDoors[i].spriteR.enabled = true;
                }
            }
        }

    }

    public void ChangeToSpecial(Color newColor){
        body.color = newColor;
    }


}
