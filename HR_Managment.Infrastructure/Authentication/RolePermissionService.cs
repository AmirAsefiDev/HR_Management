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
                Permissions.MyLeaveRequestsList,
                Permissions.LeaveRequestStatusHistoryRead
            },
            "Manager" => new[]
            {
                Permissions.LeaveAllocationCreate,
                Permissions.LeaveAllocationUpdate,
                Permissions.LeaveAllocationReadList,
                Permissions.LeaveAllocationRead,
                Permissions.LeaveAllocationDelete,

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
                Permissions.LeaveTypeDelete,

                Permissions.LeaveRequestStatusHistoryReadList,
                Permissions.LeaveRequestStatusHistoryRead,

                Permissions.LeaveStatusCreate,
                Permissions.LeaveStatusUpdate,
                Permissions.LeaveStatusReadList,
                Permissions.LeaveStatusRead,
                Permissions.LeaveStatusDelete
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

                Permissions.LeaveTypeReadList,
                Permissions.LeaveTypeRead,

                Permissions.LeaveRequestStatusHistoryReadList,
                Permissions.LeaveRequestStatusHistoryRead,

                Permissions.LeaveStatusReadList,
                Permissions.LeaveStatusRead
            },
            "Admin" => new[]
            {
                Permissions.LeaveAllocationCreate,
                Permissions.LeaveAllocationUpdate,
                Permissions.LeaveRequestChangeStatus,
                Permissions.LeaveAllocationReadList,
                Permissions.LeaveAllocationRead,
                Permissions.LeaveAllocationDelete,

                Permissions.LeaveRequestCreate,
                Permissions.LeaveRequestUpdate,
                Permissions.LeaveRequestRead,
                Permissions.LeaveRequestReadList,
                Permissions.LeaveRequestDelete,
                Permissions.MyLeaveRequestsList,

                Permissions.LeaveTypeCreate,
                Permissions.LeaveTypeUpdate,
                Permissions.LeaveTypeReadList,
                Permissions.LeaveTypeRead,
                Permissions.LeaveTypeDelete,

                Permissions.LeaveRequestStatusHistoryReadList,
                Permissions.LeaveRequestStatusHistoryRead,

                Permissions.LeaveStatusCreate,
                Permissions.LeaveStatusUpdate,
                Permissions.LeaveStatusReadList,
                Permissions.LeaveStatusRead,
                Permissions.LeaveStatusDelete
            },
            _ => Array.Empty<string>()
        };
    }
}