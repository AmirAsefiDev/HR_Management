using AutoMapper;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.DTOs.LeaveAllocation.UpdateLeaveAllocation;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Domain;

namespace HR_Management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region LeaveRequest Mapping

        CreateMap<LeaveRequest, LeaveRequestDto>()
            .ForMember(dest => dest.LeaveStatusName, otp => otp.MapFrom(src => src.LeaveStatus.Name))
            .ForMember(dest => dest.LeaveTypeName, otp => otp.MapFrom(src => src.LeaveType.Name))
            .ReverseMap();

        CreateMap<LeaveRequest, LeaveRequestListDto>()
            .ForMember(dest => dest.LeaveStatusName, otp => otp.MapFrom(src => src.LeaveStatus.Name))
            .ForMember(dest => dest.LeaveTypeName, otp => otp.MapFrom(src => src.LeaveType.Name))
            .ForMember(dest => dest.CreatorId, otp => otp.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CreatorName, otp => otp.MapFrom(src => src.User.FullName))
            .ReverseMap();

        CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();

        #endregion

        #region LeaveAllocation Mapping

        CreateMap<LeaveAllocation, LeaveAllocationDto>()
            .ForMember(dest => dest.LeaveTypeName, otp => otp.MapFrom(src => src.LeaveType.Name))
            .ReverseMap();

        CreateMap<LeaveAllocation, CreateLeaveAllocationDto>().ReverseMap();
        CreateMap<LeaveAllocation, UpdateLeaveAllocationDto>().ReverseMap();

        #endregion

        #region LeaveRequestStatusHistory Mapping

        CreateMap<LeaveRequestStatusHistory, LeaveRequestStatusHistoryDto>()
            .ForMember(dest => dest.ChangerName, otp => otp.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.LeaveRequestName, otp => otp.MapFrom(src => src.LeaveRequest.RequestComments))
            .ForMember(dest => dest.LeaveStatusName, otp => otp.MapFrom(src => src.LeaveStatus.Name))
            .ForMember(dest => dest.ChangedAt, otp => otp.MapFrom(src => src.DateCreated))
            .ReverseMap();

        #endregion

        #region LeaveType Mapping

        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();

        #endregion

        #region LeaveStatus Mapping

        CreateMap<LeaveStatus, LeaveStatusDto>().ReverseMap();
        CreateMap<LeaveStatus, CreateLeaveStatusDto>().ReverseMap();
        CreateMap<LeaveStatus, UpdateLeaveStatusDto>().ReverseMap();

        #endregion
    }
}