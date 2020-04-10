namespace MoodleVisitor.Models.Infrastructure
{
	public interface IShceduleProvider
	{
		void CreateInitialSchedule(string path);
		Shcedule Shcedule { get; }
		Shcedule GetScheduleFromFile(string path);
	}
}