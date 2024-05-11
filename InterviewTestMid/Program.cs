namespace InterviewTestMid
{
    internal class Program
    {
        public Program()
        {
            DoWork();
        }

        private void DoWork()
        {
            Logger Log = new ();
            Log.WriteLogMessage("Doing some JSON tasks...");

            //Do JSON tasks here.

            Log.WriteLogMessage("Finished doing some JSON tasks.");
        }
    }
}