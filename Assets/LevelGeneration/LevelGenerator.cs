using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class LevelGenerator : MonoBehaviour
{
    public int amountOfRooms = 1;

    public Room roomPrefab;

    public static readonly float prefabsDistance = 1;
    public readonly Vector2[] offsets = new Vector2[]{
        Vector2.up * prefabsDistance,
        Vector2.right * prefabsDistance,
        Vector2.down * prefabsDistance,
        Vector2.left * prefabsDistance
    };

    public List<Room> rooms;

    private Transform roomContainer;
    public bool generatingRooms;
    public Room generatorRoom;

    void Awake(){
        rooms = new List<Room>();
        generatorRoom = GetComponent<Room>();

        roomContainer = new GameObject("Rooms").transform;
    }

    IEnumerator Start(){
        StartCoroutine(GenerateRooms(roomPrefab));
        while(generatingRooms)
            yield return new WaitForSeconds(0.2f);
        
        GenerateDoors();
    }

    IEnumerator GenerateRooms(Room prefab){
        generatingRooms = true;
        Room.Directions dir;
        Vector2 offset;
        Vector2 last = transform.position;

        for(int i = 0; i < amountOfRooms; i++){
            dir = (Room.Directions)Random.Range(0, 4);
            offset = offsets[(int)dir];
            Vector2 newRoomPos = last + offset;

            Room newRoom = Instantiate(roomPrefab, newRoomPos, Quaternion.identity, roomContainer);
            newRoom.gameObject.name = "Room " + rooms.Count;
            yield return new WaitForSeconds(0.05f);

            last = newRoomPos;

            if(newRoom.collision){
                newRoom.gameObject.SetActive(false);
                Destroy(newRoom.gameObject);
                i--;
                continue;
            }
            rooms.Add(newRoom);
            
        }
        generatingRooms = false;
        yield return null;
        
    }


    private void GenerateDoors(){
        generatorRoom.AssignNeighbors(offsets);

        for(int i = 0; i < rooms.Count; i++){
            rooms[i].AssignNeighbors(offsets);
        }
    }
}
