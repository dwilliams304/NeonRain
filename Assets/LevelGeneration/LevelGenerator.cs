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

    public bool addHallwayBias = true;
    public int hallWayBiasWeight = 30;

    public bool finishedGeneration = false;

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
        Room.Directions lastDir = 0;
        Vector2 offset;
        Vector2 last = transform.position;

        for(int i = 0; i < amountOfRooms; i++){
            dir = (Room.Directions)Random.Range(0, 4);

            //This if statement adds bias for hallways!
            //The higher the number put into Roll100 -> the more likely it is for the next Room
            //To go in the same direction, making it more of a hallway.
            if(addHallwayBias){
                if(Extensions.Roll100(hallWayBiasWeight)){
                    switch(lastDir){
                        case 0:
                            dir = lastDir;
                            break;
                        case Room.Directions.right:
                            dir = lastDir;
                            break;
                        case Room.Directions.down:
                            dir = lastDir;
                            break;
                        case Room.Directions.left:
                            dir = lastDir;
                            break;
                    }
                }
            }

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
            if(Extensions.Roll100(specialRoomChance) == true){
                specialRooms.Add(newRoom);
                Debug.Log($"<color=green>Successfull Roll! </color>{newRoom.gameObject.name} was made into a <color=cyan>special room!</color>");
            }
            if(Extensions.Roll100(chanceForChests) == true){
                newRoom.amountOfChests = Random.Range(0, maxChestsPerRoom + 1);
                Debug.Log($"<color=green>Successfull Roll! </color>{newRoom.gameObject.name} was given: <color=yellow>{newRoom.amountOfChests} chests! </color>");
            }

            lastDir = dir;
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
