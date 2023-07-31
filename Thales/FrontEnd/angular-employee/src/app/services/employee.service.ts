import { Injectable } from '@angular/core';
import { ResponseGetEmployees, ResponseGetEmployeeById  } from '../dto/employees.dto';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor( private http: HttpClient) { }

  private  apiURLEmployments = environment.apiURLEmployments; 

  public getAll (): Observable <ResponseGetEmployees>
  {
    return this.http.get<ResponseGetEmployees> (this.apiURLEmployments )
  }

  public getEmployeeById (id: string): Observable <ResponseGetEmployeeById>
  {
    return this.http.get<ResponseGetEmployeeById> ( `${this.apiURLEmployments}${id}`)
  }

}
