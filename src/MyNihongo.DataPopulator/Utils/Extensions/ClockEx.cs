namespace MyNihongo.DataPopulator.Utils.Extensions;

internal static class ClockEx
{
	public static long GetTicksNow(this IClock @this) =>
		@this.GetCurrentInstant().ToUnixTimeTicks();
}