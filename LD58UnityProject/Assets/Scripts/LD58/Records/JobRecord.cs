using LD58.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace LD58.Records
{
    [Serializable]
    public class JobRecord
    {
        private static JobRecord current;

        public static JobRecord Current
        {
            get => current;
            set
            {
                current = value;
                OnChanged.Invoke();
            }
        }

        public IReadOnlyDictionary<JobDefinition, int> ActiveJobsIncomes { get; }
        public bool KnownIncomes { get; private set; }

        public static UnityEvent OnChanged { get; } = new();

        public JobRecord( IEnumerable<JobDefinition> jobs )
        {
            ActiveJobsIncomes = jobs.ToDictionary( k => k, k => k.RandomPay );
        }

        public void LearnIncomes()
        {
            KnownIncomes = true;
            OnChanged.Invoke();
        }

        public void ForgetIncomes()
        {
            KnownIncomes = false;
            OnChanged.Invoke();
        }
    }
}