using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BafManager : MonoBehaviour {
    
    public enum bafType { HEALING, FREEZING, SCORING};
    public bafType bType = bafType.HEALING;

    public void killBaf()
    {
        Destroy(gameObject);
    }
}
