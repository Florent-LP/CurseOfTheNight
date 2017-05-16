using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
  void enter();

  void execute();

  void exit();
}
