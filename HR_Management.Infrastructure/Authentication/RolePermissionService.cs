using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Common.Security;

namespace HR_Management.Infrastructure.Authentication;

public class RolePermissionService : IRolePermissionService
{
    public IEnumerable<string> GetPermissionsByRole(string role)
    {
        return role switch
        {
            "Employee" => new[]
            {
                Permissions.LeaveRequestCreate,
                Permissions.LeaveRequestDelete,
                Permissions.LeaveRequestRead,

                Permissions.LeaveTypeReadList,
                Permissions.LeaveTypeReadListSelection,

                Permissions.MyLeaveRequestsList,
                Permissions.LeaveRequestStatusHistoryRead,
                Permissions.UserEditProfile,
                Permissions.UserRead
            },
            "Manager" => new[]
            {
                Permissions.LeaveAllocationCreate,
                Permissions.LeaveAllocationUpdate,
                Permissions.LeaveAllocationReadList,
                Permissions.LeaveAllocationRead,
                Permissions.LeaveAllocationDelete,
                Permissions.LeaveAllocationReset,

                Permissions.LeaveRequestCreate,
                Permissions.LeaveRequestUpdate,
                Permissions.LeaveRequestChangeStatus,
                Permissions.LeaveRequestRead,
                Permissions.LeaveRequestReadList,
                Permissions.LeaveRequestDelete,
                Permissions.MyLeaveRequestsList,

                Permissions.LeaveTypeCreate,
                Permissions.LeaveTypeUpdate,
                Permissions.LeaveTypeReadList,
                Permissions.LeaveTypeRead,
                Permissions.LeaveTypeReadListSelection,
                Permissions.LeaveTypeDelete,

                Permissions.LeaveRequestStatusHistoryReadList,
                Permissions.LeaveRequestStatusHistoryRead,

                Permissions.LeaveStatusCreate,
                Permissions.LeaveStatusUpdate,
                Permissions.LeaveStatusReadList,
                Permissions.LeaveStatusReadListSelection,
                Permissions.LeaveStatusRead,
                Permissions.LeaveStatusDelete,

                Permissions.UserEditProfile,
                Permissions.UserRead,
                Permissions.UserReadList
            },
            "HR" => new[]
            {
                Permissions.LeaveRequestCreate,
                Permissions.LeaveRequestUpdate,
                Permissions.LeaveRequestRead,
                Permissions.LeaveRequestChangeStatus,
                Permissions.LeaveRequestReadList,
                Permissions.LeaveRequestDelete,
                Permissions.MyLeaveRequestsList,

                Permissions.LeaveAllocationReadList,
                Permissions.LeaveAllocationRead,
                Permissions.LeaveAllocationReset,

                Permissions.LeaveTypeReadList,
                Permissions.LeaveTypeReadListSelection,
                Permissions.LeaveTypeRead,

                Permissions.LeaveRequestStatusHistoryReadList,
                Permissions.LeaveRequestStatusHistoryRead,

                Permissions.LeaveStatusReadList,
                Permissions.LeaveStatusReadListSelection,
                Permissions.LeaveStatusRead,

                Permissions.UserEditProfile,
                Permissions.UserRead
            },
            "Admin" => new[]
            {
                Permissions.LeaveAllocationCreate,
                Permissions.LeaveAllocationUpdate,
                Permissions.LeaveRequestChangeStatus,
                Permissions.LeaveAllocationReadList,
                Permissions.LeaveAllocationRead,
                Permissions.LeaveAllocationDelete,
                Permissions.LeaveAllocationReset,

                Permissions.LeaveRequestCreate,
                Permissions.LeaveRequestUpdate,
                Permissions.LeaveRequestRead,
                Permissions.LeaveRequestReadList,
                Permissions.LeaveRequestDelete,
                Permissions.MyLeaveRequestsList,

                Permissions.LeaveTypeCreate,
                Permissions.LeaveTypeUpdate,
                Permissions.LeaveTypeReadList,
                Permissions.LeaveTypeReadListSelection,
                Permissions.LeaveTypeRead,
                Permissions.LeaveTypeDelete,

                Permissions.LeaveRequestStatusHistoryReadList,
                Permissions.LeaveRequestStatusHistoryRead,

                Permissions.LeaveStatusCreate,
                Permissions.LeaveStatusUpdate,
                Permissions.LeaveStatusReadList,
                Permissions.LeaveStatusReadListSelection,
                Permissions.LeaveStatusRead,
                Permissions.LeaveStatusDelete,

                Permissions.UserEditProfile,
                Permissions.UserRead,
                Permissions.UserReadList
            },
            _ => Array.Empty<string>()
        };
    }
}