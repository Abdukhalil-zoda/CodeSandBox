public class TaskQueue
    {
        public TaskQueue()
        {
            Running = false;
        }

        public bool Running;
        public List<TaskRun> Queue = new List<TaskRun>();

        public class TaskRun
        {
            public Func<RunCode, Task>? Func;
            public RunCode? Param;
        }

        public async Task Run()
        {
            while (Queue.Count > 0)
            {
                Running = true;
                var obj = Queue.First();
                await obj.Func!.Invoke(obj.Param!);

                Queue.RemoveAt(0);
            }
            

            Running = false;
        }

        public async void Add(Func<RunCode, Task> task, RunCode dto)
        {
            Queue.Add(new TaskRun
            {
                Func = task,
                Param = dto
            });
            if (!Running)
            {
                await Run();
            }
        }
    }
