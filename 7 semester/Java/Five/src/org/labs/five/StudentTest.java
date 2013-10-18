package org.labs.five;

import com.mysql.jdbc.Connection;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;

import java.sql.DriverManager;

import static org.junit.Assert.assertEquals;

@RunWith(JUnit4.class)
public class StudentTest {
    private Connection connect = null;

    @Test
    public void testCRUD() throws Exception {
        Students dao = new Students(connect);
        Student student = new Student();
        student.setFirstName("Ivan");
        student.setLastName("Ivanov");
        student.setPassportNumber("MP1");
        student.setYearBorn(1990);

        dao.create(student);

        assertEquals(dao.readAll().size(), 1);
        assertEquals(dao.readAll().get(0).getId(), student.getId());

        student.setPassportNumber("MP2");

        dao.update(student);

        assertEquals(dao.readAll().get(0).getPassportNumber(), student.getPassportNumber());
        assertEquals(dao.read(student.getId()).getId(), student.getId());

        dao.delete(student);
        assertEquals(dao.readAll().size(), 0);
    }

    @Before
    public void setUp() throws Exception {
        Class.forName("com.mysql.jdbc.Driver");
        connect = (Connection) DriverManager.getConnection(
                "jdbc:mysql://localhost/javalabs?user=root&password=123"
        );

    }

    @After
    public void tearDown() throws Exception {
        connect.close();
    }
}
