using System;
using LD58.Locations;
using UnityEngine;
namespace LD58.Jobs {
   [ Serializable ]
   public class Job {
      [ SerializeField ] private JobDefinition _jobDefinition;
      [ SerializeField ] private Location _location;

      public JobDefinition JobDefinition => _jobDefinition;
      public Location Location => _location;

      public Job(JobDefinition jobDefinition, Location location) {
         _jobDefinition = jobDefinition;
         _location = location;
      }
   }
}
