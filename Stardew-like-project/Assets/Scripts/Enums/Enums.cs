public enum AnimationName
{
    idleDown, 
    idleUp,
    idleRight,
    idleLeft,
    walkUp,
    walkDown,
    walkRight,
    walkLeft,
    runUp,
    runDown,
    runLeft,
    runRight,
    useToolUp,
    useToolDown,
    useToolLeft,
    useToolRight,
    swingToolUp,
    swingToolDown,
    swingToolLeft,
    swingToolRight,
    liftToolUp,
    liftToolDown,
    liftToolLeft,
    liftToolRight,
    holdToolUp,
    holdToolDown,
    holdToolLeft,
    holdToolRight,
    pickUp,
    pickDown,
    pickLeft,
    pickRight,
    count
}

public enum CharacterPartAnimator
{
    body,
    arms,
    hair,
    tool,
    hat,
    count
}

public enum PartVariantColour
{
    none,
    count
}

public enum PartVariantType
{
    none,
    carry,
    hoe,
    pickaxe,
    axe,
    scythe,
    wateringCan,
    count
}

public enum InventoryLocation
{
    player,
    chest,
    count
}

public enum ToolEffect
{
    none,
    watering
}

public enum Direction
{
    left,
    right,
    up,
    down,
    none
}

public enum ItemType
{
    Seed,
    Commodity,
    Watering_tool,
    Hoeing_tool,
    Choping_tool,
    Breaking_tool,
    Reaping_tool,
    Collecting_tool,
    Reapable_scenary,
    Furniture,
    none,
    count
}