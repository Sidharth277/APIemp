using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

[ApiController]
[Route("api/calculator")]
public class CalculatorController : ControllerBase
{
    public readonly db _db;

    public CalculatorController()
    {
        _db = new db();
    }

    private const string ConnectionString = "Data Source=SIDHARTH-LAPTOP\\SQLEXPRESS;" +
        "Initial Catalog=employ;Integrated Security=True";

    [HttpPost("post")]
    public IActionResult EnterVal([FromBody] employee request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input. Please provide valid data.");
        }

        _db.SaveEmployeeData(request.Id, request.Name, request.email, request.phone, request.dob, request.gender, request.pos);

        return Ok(new employee
        {
            Id = request.Id,
            Name = request.Name,
            email = request.email,
            phone = request.phone,
            dob = request.dob,
            gender = request.gender,
            pos = request.pos
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployeeData(string id)
    {
        employee employeeData = _db.RetrieveEmployeeById(id);

        if (employeeData == null)
        {
            return NotFound(); // Employee not found
        }

        return Ok(employeeData);
    }

   

    [HttpPatch("update/{Id}")]
    public IActionResult UpdateEmployeeData(string Id, [FromBody] employee request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input. Please provide valid data.");
        }

        _db.UpdateEmployee(Id, request);

        return Ok(new employee
        {
            Id = Id,
            Name = request.Name,
            email = request.email,
            phone = request.phone,
            dob = request.dob,
            gender = request.gender,
            pos = request.pos
        });
    }

    [HttpDelete("delete/{Id}")]
    public IActionResult DeleteEmployee(string Id)
    {
        _db.DeleteEmployeeData(Id);
        return Ok($"Employee with ID {Id} has been deleted.");
    }
}




 