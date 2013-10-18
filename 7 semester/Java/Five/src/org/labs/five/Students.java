package org.labs.five;

import com.mysql.jdbc.Connection;
import com.mysql.jdbc.Statement;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class Students {
    public Connection connection;
    private PreparedStatement createStatement, readStatement, updateStatement, deleteStatement, readAllStatement;

    public Students(Connection connection) throws SQLException {
        this.connection = connection;
        createStatement = connection.prepareStatement(
                "INSERT INTO students VALUES (default, ?, ?, ?, ?)",
                Statement.RETURN_GENERATED_KEYS
        );
        readStatement = connection.prepareStatement(
                "SELECT * FROM students WHERE id=?"
        );
        updateStatement = connection.prepareStatement(
                "UPDATE students SET firstName=?, lastName=?, passportNumber=?, yearBorn=? WHERE id=?"
        );
        deleteStatement = connection.prepareStatement(
                "DELETE FROM students WHERE id=?"
        );
        readAllStatement = connection.prepareStatement(
                "SELECT * FROM students"
        );
    }

    public void create(Student student) throws SQLException {
        createStatement.setString(1, student.getFirstName());
        createStatement.setString(2, student.getLastName());
        createStatement.setString(3, student.getPassportNumber());
        createStatement.setInt(4, student.getYearBorn());
        createStatement.executeUpdate();
        ResultSet keySet = createStatement.getGeneratedKeys();
        keySet.next();
        student.setId(keySet.getLong(1));
    }

    private Student readStudent(ResultSet rs) throws SQLException {
        Student student = new Student();
        student.setId(rs.getLong(1));
        student.setFirstName(rs.getString(2));
        student.setLastName(rs.getString(3));
        student.setPassportNumber(rs.getString(4));
        student.setYearBorn(rs.getInt(5));
        return student;
    }

    public Student read(long id) throws SQLException {
        readStatement.setLong(1, id);
        ResultSet rs = readStatement.executeQuery();
        try {
            if (!rs.next())
                return null;
            return readStudent(rs);
        } finally {
            rs.close();
        }
    }

    public List<Student> readAll() throws SQLException {
        List<Student> results = new ArrayList<Student>();
        ResultSet rs = readAllStatement.executeQuery();
        while (rs.next())
            results.add(readStudent(rs));
        rs.close();
        return results;
    }

    public void update(Student student) throws SQLException {
        updateStatement.setString(1, student.getFirstName());
        updateStatement.setString(2, student.getLastName());
        updateStatement.setString(3, student.getPassportNumber());
        updateStatement.setInt(4, student.getYearBorn());
        updateStatement.setLong(5, student.getId());
        updateStatement.execute();
    }

    public void delete(Student student) throws SQLException {
        deleteStatement.setLong(1, student.getId());
        deleteStatement.execute();
    }
}
