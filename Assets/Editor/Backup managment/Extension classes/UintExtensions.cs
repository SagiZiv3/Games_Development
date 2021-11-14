namespace UnityBackupManagment
{
    internal static class UintExtensions
    {
        internal static string GetFormattedNumber(this uint number)
        {
            return string.Format("{0:n0}", number);
        }
    }
}
