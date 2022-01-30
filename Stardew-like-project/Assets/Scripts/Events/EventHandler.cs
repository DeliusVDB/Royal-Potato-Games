using System;
using System.Collections.Generic;

public delegate void MovementDelegate(float inputX, float inputY,
    bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool idleRight, bool idleLeft, bool idleUp, bool idleDown);

public static class EventHandler
{
    // Inventory Updated Event
    public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdatedEvent;

    public static void CallInventoryUpdatedEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (InventoryUpdatedEvent != null)
        {
            InventoryUpdatedEvent(inventoryLocation, inventoryList);
        }
    }

    // Movement Event
    public static event MovementDelegate MovementEvent;

   // Movement Event Call for Publishers
   public static void CallMovementEvent(float inputX, float inputY,
    bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool idleRight, bool idleLeft, bool idleUp, bool idleDown)
    {
        if (MovementEvent != null)
        {
            MovementEvent(inputX, inputY,
                isWalking, isRunning, isIdle, isCarrying, toolEffect,
                isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                idleRight, idleLeft, idleUp, idleDown);
        }
    }
}