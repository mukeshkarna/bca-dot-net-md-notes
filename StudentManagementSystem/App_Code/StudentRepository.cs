using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class Student
{
    public int StudentID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Program { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool Active { get; set; }
    
    public string FullName 
    { 
        get { return FirstName + " " + LastName; } 
    }
}

public class StudentRepository
{
    private string connectionString;
    
    public StudentRepository()
    {
        connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }
    
    // Get all students
    public List<Student> GetAllStudents()
    {
        List<Student> students = new List<Student>();
        
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM Students WHERE Active = TRUE ORDER BY LastName, FirstName";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            
            try
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(ReadStudent(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                throw ex;
            }
        }
        
        return students;
    }
    
    // Get student by ID
    public Student GetStudentById(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM Students WHERE StudentID = @StudentID AND Active = TRUE";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentID", id);
            
            try
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadStudent(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                throw ex;
            }
        }
        
        return null;
    }
    
    // Insert student
    public int AddStudent(Student student)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO Students 
                           (FirstName, LastName, Email, DateOfBirth, Gender, Program, EnrollmentDate, Active) 
                           VALUES 
                           (@FirstName, @LastName, @Email, @DateOfBirth, @Gender, @Program, @EnrollmentDate, TRUE);
                           SELECT LAST_INSERT_ID();";
            
            MySqlCommand cmd = new MySqlCommand(query, conn);
            AddStudentParameters(cmd, student);
            
            try
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Log error
                throw ex;
            }
        }
    }
    
    // Update student
    public bool UpdateStudent(Student student)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE Students 
                           SET FirstName = @FirstName, 
                               LastName = @LastName, 
                               Email = @Email, 
                               DateOfBirth = @DateOfBirth, 
                               Gender = @Gender, 
                               Program = @Program, 
                               EnrollmentDate = @EnrollmentDate
                           WHERE StudentID = @StudentID";
            
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
            AddStudentParameters(cmd, student);
            
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log error
                throw ex;
            }
        }
    }
    
    // Delete student (soft delete)
    public bool DeleteStudent(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string query = "UPDATE Students SET Active = FALSE WHERE StudentID = @StudentID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentID", id);
            
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log error
                throw ex;
            }
        }
    }
    
    // Helper method to read a student from DataReader
    private Student ReadStudent(MySqlDataReader reader)
    {
        return new Student
        {
            StudentID = Convert.ToInt32(reader["StudentID"]),
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            Email = reader["Email"].ToString(),
            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
            Gender = reader["Gender"].ToString(),
            Program = reader["Program"].ToString(),
            EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]),
            Active = Convert.ToBoolean(reader["Active"])
        };
    }
    
    // Helper method to add student parameters to command
    private void AddStudentParameters(MySqlCommand cmd, Student student)
    {
        cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
        cmd.Parameters.AddWithValue("@LastName", student.LastName);
        cmd.Parameters.AddWithValue("@Email", student.Email);
        cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
        cmd.Parameters.AddWithValue("@Gender", student.Gender);
        cmd.Parameters.AddWithValue("@Program", student.Program);
        cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);
    }
}