using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SceneSave
{
    //public Dictionary<string, List<SceneItem>> listSceneItemDictionary;

    // string key is an identifier name we choose for this list
    public List<SceneItem> listSceneItem;
    public Dictionary<string, GridPropertyDetails> gridPropertyDetailsDictionary;
}
