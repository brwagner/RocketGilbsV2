using System.Collections.Generic;

public class ItemData {
  public string name;
  public TransformData transformData = new TransformData();
  public ComponentData componentData = new ComponentData();
  public List<ItemData> children = new List<ItemData>();
}