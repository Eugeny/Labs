<!DOCTYPE html>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://tiles.apache.org/tags-tiles" prefix="tiles" %>

<html>
  <head>
    <!--link href="http://netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet"-->
  </head>
  <body>
    <nav class="navbar navbar-default" role="navigation">
        <div class="container">
          <ul class="nav navbar-nav">
            <li><a href="index.jsp">List</a></li>
            <li><a href="add.jsp">Add</a></li>
          </ul>
        </div>
    </nav>
    <div class="container">
        <tiles:insertAttribute name="content" />
    </div>
  </body>
</html>