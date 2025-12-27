using System.Collections.Generic;

namespace OPL_Manager;

public class GameRatingTag
{
	private int currentID;

	private List<GameRatingClass> _RatingSystem;

	public GameRatingTag(int val, string system)
	{
		currentID = val;
		_RatingSystem = ConfigClass.StaticRatings[system];
	}

	public GameRatingClass SelectedItem()
	{
		return _RatingSystem[currentID];
	}

	public int IncreaseCurrent()
	{
		if (currentID + 1 > _RatingSystem.Count - 1)
		{
			currentID = 0;
		}
		else
		{
			currentID++;
		}
		return currentID;
	}

	public int DecreaseCurrent()
	{
		if (currentID - 1 < 0)
		{
			currentID = _RatingSystem.Count - 1;
		}
		else
		{
			currentID--;
		}
		return currentID;
	}
}
