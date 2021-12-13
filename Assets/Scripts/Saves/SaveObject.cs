using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class SaveObject
{
	public abstract bool isInstantiatable();

	public abstract string GetKey();

	public virtual string GetPrefabPath()
	{
		return "";
	}
}