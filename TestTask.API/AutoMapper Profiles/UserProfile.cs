using AutoMapper;
using System.Text;
using TestTask.BLL.DTO;
using BLL = TestTask.BLL.Models;
using DAL = TestTask.DAL.Models;
using DTO = TestTask.BLL.DTO;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<BLL.User, DAL.User>().ReverseMap();
		CreateMap<DTO.UserDTO, BLL.User>().ReverseMap();
		CreateMap<DTO.UserDTO, DAL.User>().ReverseMap();
		CreateMap<RegisterDTO, BLL.User>().ReverseMap();
		CreateMap<UpdateDTO, BLL.User>().ReverseMap();


		CreateMap<DAL.Login, BLL.Login>().ReverseMap();
		CreateMap<DAL.Login, DTO.LogDTO>().ReverseMap();
		CreateMap<BLL.Login, DTO.LogDTO>().ReverseMap();
		CreateMap<DAL.LoginsUser, DTO.LogDTO>().ReverseMap();

		CreateMap<DAL.User, DAL.User>()
		.ForMember(dest => dest.Id, opt => opt.Ignore())
		.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => (
			srcMember != null && !string.IsNullOrWhiteSpace(srcMember.ToString())
		)));
	}
}