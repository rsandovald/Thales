import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/dto/employees.dto';
import { EmployeeService } from 'src/app/services/employee.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  employees: Employee[] | undefined;
  serviceResult: string | undefined;
  searchId: string  = "" ; 
  formTitle: string = "" ; 

  constructor(private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.searchId = params['id'];      
      
      if (this.searchId == undefined || this.searchId == "")
      {   
        this.searchId = "";      
        this.getAll();
      }
      else
        this.getEmployeeById();
    });
  }

  getAll(): void {
    this.formTitle = "Employees:" ; 
    this.employeeService.getAll().subscribe(
      response => {
        this.serviceResult = response.transactionResult.code;
        this.employees = response.employees;
      },
      error => this.serviceResult = "500" // No managed error
    );
  }

  getEmployeeById(): void {
    this.formTitle = "Employee Id: " + + this.searchId; 
    this.employeeService.getEmployeeById(this.searchId).subscribe(
      response => {
        this.serviceResult = response.transactionResult.code;
        this.employees = [response.employee];
      },
      error => 
        this.serviceResult = "500"// No managed error
    );
  }

}
