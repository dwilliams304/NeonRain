using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class LevelGenerator : MonoBehaviour
{
    public int amountOfRooms = 1;
    public int specialRoomChance = 10;
    public int chanceForChests = 10;
    public int maxChestsPerRoom = 4;

    public Room roomPrefab;

    public Color specialColor;

    public static readonly float prefabsDistance = 1;
    public readonly Vector2[] offsets = new Vector2[]{
        Vector2.up * prefabsDistance,
        Vector2.right * prefabsDistance,
        Vector2.down * prefabsDistance,
        Vector2.left * prefabsDistance
    };

    public List<Room> rooms;
    public List<Room> specialRooms;

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
        if(specialRooms.Count > 0) { MakeSpecialRoom(); }
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
            
            yield return new WaitForSeconds(0.02f);

            last = newRoomPos;

            if(newRoom.collision){
                newRoom.gameObject.SetActive(false);
                Destroy(newRoom.gameObject);
                i--;
                continue;
            }
            if(Extensions.ChanceRoll(specialRoomChance) == true){
                specialRooms.Add(newRoom);
                Debug.Log($"<color=green>Successfull Roll! </color>{newRoom.gameObject.name} was made into a <color=cyan>special room!</color>");
            }
            if(Extensions.ChanceRoll(chanceForChests) == true){
                newRoom.amountOfChests = Random.Range(0, maxChestsPerRoom + 1);
                Debug.Log($"<color=green>Successfull Roll! </color>{newRoom.gameObject.name} was given: <color=yellow>{newRoom.amountOfChests} chests! </color>");
            }
            rooms.Add(newRoom);
            
        }
        generatingRooms = false;
        yield return null;
        
    }


    void GenerateDoors(){
        generatorRoom.AssignNeighbors(offsets);

        for(int i = 0; i < rooms.Count; i++){
            rooms[i].AssignNeighbors(offsets);
        }
    }


    void MakeSpecialRoom(){
        Debug.Log($"<color=green> We have <b>{specialRooms.Count}</b> special rooms! </color>");
        foreach(Room room in specialRooms){
            room.ChangeToSpecial(specialColor);
        }
    }
}
