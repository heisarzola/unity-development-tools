using System.Collections.Generic;
using UnityEngine;


namespace FavouritesEd
{
	public class FavouritesAsset: ScriptableObject
	{
		public List<FavouritesElement> favs = new List<FavouritesElement>();
		public List<FavouritesCategory> categories = new List<FavouritesCategory>();
		[SerializeField] private int nextCategoryId = 0;

		public FavouritesCategory AddCategory(string name)
		{
			FavouritesCategory c = new FavouritesCategory()
			{
				id = nextCategoryId,
				name = name,
			};

			nextCategoryId++;
			categories.Add(c);

			return c;
		}

		// ------------------------------------------------------------------------------------------------------------
	}
}
