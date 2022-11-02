using P7WebApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class Permission : EntityBase
    {
        public Permission()
        {
            
        }
        public bool CanUpdateCourse { get; private set; }
        public bool CanAddExerciseGroup { get; private set; }
        public bool CanDeleteExerciseGroup { get; private set; }
        public bool CanAddExercise { get; private set; }
        public bool CanUpdateExercise { get; private set; }
        public bool CanDeleteExercise { get; private set; }
        public bool CanCreateIniviteCode { get; private set; }
        public bool CanRevokeInviteCode { get; private set; }
        public bool CanViewSubmission { get; private set; }
        public bool CanDeleteSubmission { get; private set; }
        public bool CanCreateRoles { get; private set; }
        public bool CanRemoveAttendee { get; private set; }
    }
}
