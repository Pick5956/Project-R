using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static StructureHelper;

public class CorridorNode : Node
{
    private Node structure1;
    private Node structure2;
    private int corridorWidth;
    private int modifierDistanceFromWall = 1;

    public CorridorNode(Node node1, Node node2, int corridorWidth) : base(null)
    {
        this.structure1 = node1;
        this.structure2 = node2;
        this.corridorWidth = corridorWidth;
        GenerateCorridor();
    }

    private void GenerateCorridor()
    {
        var relativePositionOfStructure2 = CheckPositionStructure2AgainstStructure1();
        switch (relativePositionOfStructure2)
        {
            case RealativePosition.Up:
                ProcessRoomInRelationUpOrDown(this.structure1, this.structure2);
                break;
            case RealativePosition.Down:
                ProcessRoomInRelationUpOrDown(this.structure2, this.structure1);
                break;
            case RealativePosition.Right:
                ProcessRoomInRelationRightOrLeft(this.structure1, this.structure2);
                break;
            case RealativePosition.Left:
                ProcessRoomInRelationRightOrLeft(this.structure2, this.structure1);
                break;
            default:
                break;
        }
    }

    private void ProcessRoomInRelationUpOrDown(Node structure1,Node structure2)
    {
        Node bottomStructure = null;
        List<Node> structureBottomChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node topStructure = null;
        List<Node> topStructureAboveChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        var sortedBottomStructure = structureBottomChildren.OrderByDescending(child => child.TopRightAreaCorner.y).ToList();
        if (sortedBottomStructure.Count == 1)
        {
            bottomStructure = structureBottomChildren[0];
        }
        else
        {
            int maxY = sortedBottomStructure[0].TopLeftAreaCorner.y;
            sortedBottomStructure = sortedBottomStructure.Where(children => Mathf.Abs(maxY - children.TopLeftAreaCorner.y) < 10).ToList();
            int index = UnityEngine.Random.Range(0, sortedBottomStructure.Count);
            bottomStructure = sortedBottomStructure[index];
        }
        var possibleNeighboursInRightStructureList = topStructureAboveChildren.Where(
           child => GetValidXForNeigbourUpDown(
               bottomStructure.TopRightAreaCorner,
               bottomStructure.BottomRightAreaCorner,
               child.BottomLeftAreaCorner,
               child.BottomRightAreaCorner) != -1
           ).OrderBy(child => child.BottomRightAreaCorner.y).ToList();
        if (possibleNeighboursInRightStructureList.Count == 0)
        {
            topStructure = structure2;
        }
        else
        {
            topStructure = possibleNeighboursInRightStructureList[0];
        }
        int x = GetValidXForNeigbourUpDown(
            bottomStructure.TopLeftAreaCorner,
            bottomStructure.TopRightAreaCorner,
            topStructure.BottomLeftAreaCorner,
            topStructure.BottomRightAreaCorner);
        while(x == -1 && sortedBottomStructure.Count > 1)
        {
            sortedBottomStructure = sortedBottomStructure.Where(child => child.TopLeftAreaCorner.x != topStructure.TopLeftAreaCorner.x).ToList();
            bottomStructure = sortedBottomStructure[0];
            x = GetValidXForNeigbourUpDown(
            bottomStructure.TopLeftAreaCorner,
            bottomStructure.TopRightAreaCorner,
            topStructure.BottomLeftAreaCorner,
            topStructure.BottomRightAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(x, bottomStructure.TopLeftAreaCorner.y);
        TopRightAreaCorner = new Vector2Int(x+this.corridorWidth,topStructure.BottomLeftAreaCorner.y);

    }

    private int GetValidXForNeigbourUpDown
        (Vector2Int bottomNodeLeft, Vector2Int bottomNodeRight, Vector2Int topNodeLeft, Vector2Int topNodeRight)
    {
        if (topNodeLeft.x < bottomNodeLeft.x && bottomNodeRight.x < topNodeRight.x)
        {
            return StructureHelper.CalculatemiddlePoint(
                bottomNodeLeft + new Vector2Int(modifierDistanceFromWall,0),
                bottomNodeRight - new Vector2Int(modifierDistanceFromWall + this.corridorWidth,0))
                .x;
        }
        if (topNodeLeft.x >= bottomNodeLeft.x && bottomNodeRight.x >= topNodeRight.x)
        {
            return StructureHelper.CalculatemiddlePoint(
                topNodeLeft + new Vector2Int(modifierDistanceFromWall, 0),
                topNodeRight - new Vector2Int(modifierDistanceFromWall + this.corridorWidth, 0))
                .x;
        }
        if (bottomNodeLeft.x >= topNodeLeft.x && bottomNodeLeft.x <= topNodeRight.x)
        {
            return StructureHelper.CalculatemiddlePoint(
                bottomNodeLeft + new Vector2Int(modifierDistanceFromWall, 0),
                topNodeRight - new Vector2Int(modifierDistanceFromWall + this.corridorWidth, 0))
                .x;
        }
        if (bottomNodeRight.x <= topNodeRight.x && bottomNodeRight.x >= topNodeLeft.x)
        {
            return StructureHelper.CalculatemiddlePoint(
                topNodeLeft + new Vector2Int(modifierDistanceFromWall, 0),
                bottomNodeRight - new Vector2Int(modifierDistanceFromWall + this.corridorWidth, 0))
                .x;
        }
        return -1;
    }

    private void ProcessRoomInRelationRightOrLeft(Node structure1, Node structure2)
    {
        Node leftStructure = null;
        List<Node> leftStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure1);
        Node rightStructure = null;
        List<Node> rightStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(structure2);

        var sortedLeftStructure = leftStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();
        if (sortedLeftStructure.Count == 1)
        {
            leftStructure = sortedLeftStructure[0];

        }
        else
        {
            int maxX = sortedLeftStructure[0].TopRightAreaCorner.x;
            sortedLeftStructure = sortedLeftStructure.Where(children => Mathf.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();
            int index = UnityEngine.Random.Range(0,sortedLeftStructure.Count);
            leftStructure = sortedLeftStructure[index];
        }

        var possibleNeighboursInRightStructureList = rightStructureChildren.Where(
            child => GetValidYForNeigbourLeftRight(
                leftStructure.TopRightAreaCorner,
                leftStructure.BottomRightAreaCorner,
                child.TopLeftAreaCorner,
                child.BottomLeftAreaCorner) != -1
            ).OrderBy(child => child.BottomRightAreaCorner.x).ToList();
        if (possibleNeighboursInRightStructureList.Count <= 0)
        {
            rightStructure = structure2;
        }
        else
        {
            rightStructure = possibleNeighboursInRightStructureList[0];
        }
        int y = GetValidYForNeigbourLeftRight(
            leftStructure.TopLeftAreaCorner,
            leftStructure.BottomRightAreaCorner,
            rightStructure.TopLeftAreaCorner,
            rightStructure.BottomLeftAreaCorner);
        while (y == -1 && sortedLeftStructure.Count > 0)
        {
            sortedLeftStructure = sortedLeftStructure.Where(child => child.TopLeftAreaCorner.y != leftStructure.TopLeftAreaCorner.y).ToList();
            leftStructure = sortedLeftStructure[0];
            y = GetValidYForNeigbourLeftRight(
            leftStructure.TopLeftAreaCorner,
            leftStructure.BottomRightAreaCorner,
            rightStructure.TopLeftAreaCorner,
            rightStructure.BottomLeftAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(leftStructure.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStructure.TopLeftAreaCorner.x, y + this.corridorWidth);

    }

    private int GetValidYForNeigbourLeftRight(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int RightNodeUp, Vector2Int RightNodeDown)
    {
        if (RightNodeUp.y >= leftNodeUp.y && leftNodeDown.y  >= RightNodeDown.y)
        {
            return StructureHelper.CalculatemiddlePoint(
                leftNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                leftNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }
        if (RightNodeUp.y <= leftNodeUp.y && leftNodeDown.y <= RightNodeDown.y)
        {
            return StructureHelper.CalculatemiddlePoint(
                RightNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                RightNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }
        if (leftNodeUp.y >= RightNodeDown.y && leftNodeUp.y <= RightNodeUp.y)
        {
            return StructureHelper.CalculatemiddlePoint(
                RightNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                leftNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }
        if (leftNodeDown.y >= RightNodeDown.y && leftNodeDown.y <= RightNodeUp.y)
        {
            return StructureHelper.CalculatemiddlePoint(
               leftNodeDown + new Vector2Int(0, modifierDistanceFromWall),
               RightNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }
        return -1;
    }

    private RealativePosition CheckPositionStructure2AgainstStructure1() {

        Vector2 middlePointStructure1Temp = (((Vector2)structure1.TopRightAreaCorner + structure1.BottomLeftAreaCorner) / 2);
        Vector2 middlePointStructure2Temp = (((Vector2)structure2.TopRightAreaCorner + structure2.BottomLeftAreaCorner) / 2);

        float angle = CalculateAngle(middlePointStructure1Temp, middlePointStructure2Temp);

        if (angle < 45 && angle >= 0 || angle > -45 && angle < 0)
        {
            return RealativePosition.Right;
        }
        else if (angle > 45 && angle < 135)
        {
            return RealativePosition.Up;
        }
        else if (angle > -135 && angle < -45)
        {
            return RealativePosition.Down;
        }else
        {
            return RealativePosition.Left;
        }
    }

    private float CalculateAngle(Vector2 middlePointStructure1Temp, Vector2 middlePointStructure2Temp) {
    
        return Mathf.Atan2(middlePointStructure2Temp.y - middlePointStructure1Temp.y,
            middlePointStructure2Temp.x - middlePointStructure1Temp.x)*Mathf.Rad2Deg;
    }
}