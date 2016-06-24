using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Retexture : MonoBehaviour
{

	[System.Serializable]
	public class ImageItem
	{
		public string _name;
		public Sprite _sprite;
	}

	public List<ImageItem> m_Items = new List<ImageItem> ();

	/// <summary>
	/// Returns all names for the type of sprite selection
	/// </summary>
	/// <returns>The names.</returns>
	public string[] ReturnAllNames ()
	{
		List<string> names = new List<string> ();
		foreach (ImageItem item in m_Items) {
			names.Add (item._name);
		}
		return names.ToArray ();
	}

}