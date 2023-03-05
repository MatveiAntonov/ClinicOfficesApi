namespace Events
{
	public class PhotoAddedResponse
	{
		public int Id { get; set; }
		public string PhotoUrl { get; set; } = String.Empty;
		public string PhotoName { get; set; } = String.Empty;
	}
}
