using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Permission : EntityBase
    {
        public Permission(
            int courseRoleId, 
            bool canUpdateCourse = false, 
            bool canAddExerciseGroup = false, 
            bool canDeleteExerciseGroup = false, 
            bool canAddExercise = false, 
            bool canUpdateExercise = false, 
            bool canDeleteExercise = false, 
            bool canCreateIniviteCode = false, 
            bool canRevokeInviteCode = false, 
            bool canViewSubmission = false,
            bool canDeleteSubmission = false,
            bool canCreateRoles = false ,
            bool canRemoveAttendee = false)
        {
            CourseRoleId = courseRoleId;
            CanUpdateCourse = canUpdateCourse;
            CanAddExerciseGroup = canAddExerciseGroup;
            CanDeleteExerciseGroup = canDeleteExerciseGroup;
            CanAddExercise = canAddExercise;
            CanUpdateExercise = canUpdateExercise;
            CanDeleteExercise = canDeleteExercise;
            CanCreateIniviteCode = canCreateIniviteCode;
            CanRevokeInviteCode = canRevokeInviteCode;
            CanViewSubmission = canViewSubmission;
            CanDeleteSubmission = canDeleteSubmission;
            CanCreateRoles = canCreateRoles;
            CanRemoveAttendee = canRemoveAttendee;
        }

        public int CourseRoleId { get; set; }
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
