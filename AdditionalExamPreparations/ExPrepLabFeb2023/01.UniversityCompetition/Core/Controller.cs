using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<ISubject> subjects;
        private IRepository<IStudent> students;
        private IRepository<IUniversity> universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject) && subjectType != nameof(HumanitySubject) && subjectType != nameof(TechnicalSubject))
            {
                return $"Subject type {subjectType} is not available in the application!";
            }
            if (subjects.Models.Any(s => s.Name == subjectName))
            {
                return $"{subjectName} is already added in the repository.";
            }

            ISubject subject = null;
            int subjectID = subjects.Models.Count() +1;

            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjectID, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjectID, subjectName);
            }
            else if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(subjectID, subjectName);
            }
            subjects.AddModel(subject);

            return $"{subjectType} {subjectName} is created and added to the {nameof(SubjectRepository)}!";
        }
        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.Models.Any(u => u.Name == universityName))
            {
                return $"{universityName} is already added in the repository.";
            }

            List<int> requiredSubjectsId = new List<int>();

            foreach (string requiredSubject in requiredSubjects)
            {
                requiredSubjectsId.Add(subjects.FindByName(requiredSubject).Id);
            }

            int univercityID = universities.Models.Count() + 1;

            IUniversity university = new University(univercityID, universityName, category, capacity, requiredSubjectsId);
            universities.AddModel(university);

            return $"{universityName} university is created and added to the {nameof(UniversityRepository)}!";
        }


        public string AddStudent(string firstName, string lastName)
        {
            if (students.Models.Any(s => s.FirstName == firstName && s.LastName == lastName))
            {
                return $"{firstName} {lastName} is already added in the repository.";
            }

            int studentID = students.Models.Count() + 1;

            IStudent student = new Student(studentID, firstName, lastName);
            students.AddModel(student);

            return $"Student {firstName} {lastName} is added to the {nameof(StudentRepository)}!";
        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (!students.Models.Any(s => s.Id == studentId))
            {
                return "Invalid student ID!";
            }
            if (!subjects.Models.Any(s => s.Id == subjectId))
            {
                return "Invalid subject ID!";
            }

            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student.CoveredExams.Contains(subjectId))
            {
                return $"{student.FirstName} {student.LastName} has already covered exam of {subject.Name}.";
            }

            student.CoverExam(subject);

            return $"{student.FirstName} {student.LastName} covered {subject.Name} exam!";
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] studentFullName = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string studentFirstName = studentFullName[0];
            string studentLastName = studentFullName[1];

            if (!students.Models.Contains(students.FindByName(studentName)))
            {
                return $"{studentFirstName} {studentLastName} is not registered in the application!";
            }
            if (!universities.Models.Contains(universities.FindByName(universityName)))
            {
                return $"{universityName} is not registered in the application!";
            }

            IStudent student = students.FindByName(studentName);
            IUniversity university = universities.FindByName(universityName);

            foreach (int subject in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(subject))
                {
                    return $"{studentName} has not covered all the required exams for {universityName} university!";
                }
            }

            if (student.University is not null && student.University.Name == universityName)
            {
                return $"{studentFirstName} {studentLastName} has already joined {universityName}.";
            }

            student.JoinUniversity(university);

            return $"{studentFirstName} {studentLastName} joined {universityName} university!";
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");

            int studentsCount = 0;

            foreach (IStudent student in students.Models)
            {
                if (student.University is null)
                {
                    continue;
                }
                if (student.University.Name == university.Name)
                {

                    studentsCount++;

                }
            }

            sb.AppendLine($"Students admitted: {studentsCount}");

            sb.AppendLine($"University vacancy: {university.Capacity - studentsCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
