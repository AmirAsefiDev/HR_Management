namespace HR_Management.Common.Security;

public static class Permissions
{
    public const string LeaveAllocationCreate = "LeaveAllocation.Create";
    public const string LeaveAllocationUpdate = "LeaveAllocation.Update";
    public const string LeaveAllocationReadList = "LeaveAllocation.ReadList";
    public const string LeaveAllocationRead = "LeaveAllocation.Read";
    public const string LeaveAllocationDelete = "LeaveAllocation.Delete";

    public const string LeaveRequestCreate = "LeaveRequest.Create";
    public const string LeaveRequestUpdate = "LeaveRequest.Update";
    public const string LeaveRequestChangeStatus = "LeaveRequest.ChangeStatus";
    public const string LeaveRequestRead = "LeaveRequest.Read";
    public const string LeaveRequestReadList = "LeaveRequest.ReadList";
    public const string MyLeaveRequestsList = "LeaveRequest.MyList";
    public const string LeaveRequestDelete = "LeaveRequest.Delete";

    public const string LeaveTypeCreate = "LeaveType.Create";
    public const string LeaveTypeUpdate = "LeaveType.Update";
    public const string LeaveTypeReadList = "LeaveType.ReadList";
    public const string LeaveTypeReadListSelection = "LeaveType.ReadListSelection";
    public const string LeaveTypeRead = "LeaveType.Read";
    public const string LeaveTypeDelete = "LeaveType.Delete";

    public const string LeaveRequestStatusHistoryReadList = "LeaveRequestStatusHistory.ReadList";
    public const string LeaveRequestStatusHistoryRead = "LeaveRequestStatusHistory.Read";

    public const string LeaveStatusCreate = "LeaveStatus.Create";
    public const string LeaveStatusUpdate = "LeaveStatus.Update";
    public const string LeaveStatusReadList = "LeaveStatus.ReadList";
    public const string LeaveStatusReadListSelection = "LeaveStatus.ReadListSelection";
    public const string LeaveStatusRead = "LeaveStatus.Read";
    public const string LeaveStatusDelete = "LeaveStatus.Delete";

    public const string UserEditProfile = "User.EditProfile";
    public const string UserReadList = "User.ReadList";
    public const string UserRead = "User.Read";
}