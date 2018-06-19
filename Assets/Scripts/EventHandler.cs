using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EventHandler {
    void OnEvent(params object[] data);
}
