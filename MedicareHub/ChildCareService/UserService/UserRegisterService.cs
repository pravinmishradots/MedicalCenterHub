using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildCareService.UserService
{
   
    public class UserRegisterService
    {
        //private readonly IRepo<SubjectMaster> _SubjectMasterRepo;

        //public UserRegisterService(IRepository<SubjectMaster> SubjectMasterRepo) {
        //    _SubjectMasterRepo = SubjectMasterRepo;

        //}
        //public List<SubjectMaster> GetSubjectMaster()
        //{
        //    return _SubjectMasterRepo.Query().Get().ToList();
        //}



        //public List<SubjectMaster> GetSubjectMapList()
        //{
        //    return _SubjectMasterRepo.Query().Get().ToList();
        //}


        //public IEnumerable<SubjectMaster> GetSubjectMasterPresenter()
        //{
        //    return _SubjectMasterRepo.Query().Get();
        //}

        //public bool DeleteSubjectMaster(int id)
        //{
        //    try
        //    {
        //        _SubjectMasterRepo.Delete(id);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public SubjectMaster SaveSubjectMaster(SubjectMaster subjectmaster)
        //{
        //    if (subjectmaster.SubjectId > 0)
        //    {
        //        _SubjectMasterRepo.Update(subjectmaster);
        //    }
        //    else
        //    {
        //        _SubjectMasterRepo.Insert(subjectmaster);
        //    }
        //    return subjectmaster;
        //}

        //public SubjectMaster FindById(int id)
        //{
        //    return _SubjectMasterRepo.FindById(id);
        //}
        //public SubjectMaster GetSubjectMasterPresenter(int id)
        //{
        //    return _SubjectMasterRepo.FindById(id);
        //}
        //public SubjectMaster SaveSubjectMasterPresenter(SubjectMaster subjectmaster)
        //{
        //    _SubjectMasterRepo.Insert(subjectmaster);
        //    return subjectmaster;
        //}

        //public SubjectMaster UpdateSubjectMasterPresenter(SubjectMaster subjectmaster)
        //{
        //    _SubjectMasterRepo.Update(subjectmaster);
        //    return subjectmaster;
        //}



        //// create method for Delete class when this field is work other table FK then use this concept for Delete 

        //public int DeleteSubjectMasterPresenter(int id)
        //{

        //    var subjectclassmap = _classSubjectMapRepo.Query().Filter(x => x.SubjectId == id).Get().Select(x => x.MapId).ToList();
        //    foreach (var item in subjectclassmap)
        //    {
        //        _classSubjectMapRepo.Delete(item);
        //    }

        //    var subjectclassDepartmentmap = _subjectclassdepartmentRope.Query().Filter(x => x.SubjectId == id).Get().Select(x => x.Mapping_Id).ToList();
        //    foreach (var item in subjectclassDepartmentmap)
        //    {
        //        _subjectclassdepartmentRope.Delete(item);
        //    }
        //    var facultyallocationSubject = _FacultyClassAllocationRepo.Query().Filter(x => x.SubjectId == id).Get().Select(x => x.FacultyAllocationId).ToList();
        //    foreach (var item in facultyallocationSubject)
        //    {
        //        _subjectclassdepartmentRope.Delete(item);
        //    }



        //    _SubjectMasterRepo.Delete(id);
        //    return id;

        //}



        //public PagedListResult<SubjectMaster> GetSubjectMasterList(SearchQuery<SubjectMaster> query, out int totalItems)
        //{
        //    return _SubjectMasterRepo.Search(query, out totalItems);
        //}

        //public List<SubjectMaster> GetAllSubjectList(bool IsAllSubject = false)
        //{
        //    return IsAllSubject ? _SubjectMasterRepo.Query().Get().OrderBy(x => x.Subject).ToList() : _SubjectMasterRepo.Query().Filter(x => x.IsActive == true).Get().OrderBy(x => x.Subject).ToList();
        //}


    }
}
