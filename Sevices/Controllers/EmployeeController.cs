using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Sevices.Models;
using static Azure.Core.HttpHeader;

namespace Sevices.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly DbEmpContext _dbContext;
        private  ResponseModel responseModel ;
        private IMapper _mapper ;

        public EmployeeController(DbEmpContext dbContext,IMapper mapper ) {

            _dbContext = dbContext;
            _mapper = mapper;
            responseModel= new ResponseModel();

        }
        [HttpGet]
        public ResponseModel GetAll()
        {
            try
            {
                IEnumerable<Tblemployee> empList = _dbContext.Tblemployee.ToList();
                responseModel.Result = _mapper.Map<IEnumerable<EmployeeDetails>>(empList);
                responseModel.IsSuccess = true;
                responseModel.Message = "Employee List";
            }catch( Exception ex )
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;


        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseModel Get(int id)
        {
            try
            {
                Tblemployee empList = _dbContext.Tblemployee.First(emp=>emp.EmployeeId==id);
                responseModel.Result = _mapper.Map<EmployeeDetails>(empList);
                responseModel.IsSuccess = true;
                responseModel.Message = "Employee List";
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;


        }

        [HttpGet]
        [Route("GetByName/{username}")]
        public ResponseModel GetbyName(string? username)
        {
            try
            {
                Tblemployee empList = _dbContext.Tblemployee.FirstOrDefault(emp => emp.Username.ToLower() == username.ToLower());
                if(empList == null)
                {
                    responseModel.IsSuccess = false;
                }
                responseModel.Result = _mapper.Map<EmployeeDetails>(empList);
                responseModel.IsSuccess = true;
                responseModel.Message = "Employee List";
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;


        }

        [HttpPost]
        [Route("add")]

        public ResponseModel Add([FromBody] EmployeeDetails employee )
        {
            try
            {
                Tblemployee emp= _dbContext.Tblemployee.FirstOrDefault(emp => emp.Email == employee.Email );
                if(emp==null)
                {
                    Tblemployee empList = _mapper.Map<Tblemployee>(employee);
                    empList.IsActive = true;
                    empList.IsDeleted = false;
                    empList.CreatedOn = DateTime.Now;
                    empList.CreatedBy = 1;
                    _dbContext.Tblemployee.Add(empList);
                    _dbContext.SaveChanges();
                    responseModel.Result = _mapper.Map<EmployeeDetails>(empList);
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Employee Added successfully";
                }
                else {

                    responseModel.IsSuccess = true;
                    responseModel.Message = "Employee already exits";
                }
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                throw new Exception(ex.Message);
            }
            return responseModel;


        }
        [HttpPost]
        [Route("update")]

        public ResponseModel Update([FromBody] EmployeeDetails employee)
        {
            try
            {
                Tblemployee emp = _dbContext.Tblemployee.First(emp => emp.EmployeeId == employee.EmployeeId);
                if (emp != null)
                {
                    Tblemployee empList = _mapper.Map<Tblemployee>(employee);
                    empList.IsActive = true;
                    empList.IsDeleted = false;
                    empList.CreatedOn = DateTime.Now;
                    empList.CreatedBy = 1;
                    _dbContext.Tblemployee.Update(empList);
                    _dbContext.SaveChanges();
                    responseModel.Result = _mapper.Map<EmployeeDetails>(empList);
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Employee Updated successfully";
                }
                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Employee Not Found";
                }
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
                throw new Exception(ex.Message);
            }
            return responseModel;


        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseModel Delete(int id)
        {
            try
            {
                Tblemployee obj = _dbContext.Tblemployee.First(u => u.EmployeeId == id);
                _dbContext.Tblemployee.Remove(obj);
                _dbContext.SaveChanges();
                responseModel.IsSuccess = true;
                responseModel.Message = "Deleted Successfully";

            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }
    }
}
