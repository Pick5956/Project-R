
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator
{
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dunWidth;
    private int dunLength;


    public DungeonGenerator(int dunWidth, int dunLength)
    {
        this.dunWidth = dunWidth;
        this.dunLength = dunLength;
    }

    public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset , int corridorWidth)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dunWidth, dunLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations,roomWidthMin, roomLengthMin);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations,roomWidthMin,roomLengthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerModifier, roomOffset);

        CorridorsGenerator corridorsGenerator = new CorridorsGenerator();
        var corridorList = corridorsGenerator.CreateCorridor(allSpaceNodes, corridorWidth);
        
        return new  List<Node>(roomList).Concat(corridorList).ToList();
    }
}
