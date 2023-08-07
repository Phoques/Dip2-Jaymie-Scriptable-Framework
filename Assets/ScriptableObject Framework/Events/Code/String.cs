using UnityEngine;

namespace Event
{
	[CreateAssetMenu(fileName = "Event", menuName = "Events/String Event")]
	public class String : GenericEvent<string> { }
}