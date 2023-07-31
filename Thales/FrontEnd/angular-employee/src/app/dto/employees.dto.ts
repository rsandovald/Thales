 
 
  export interface Employee {
    id: string
    name: string
    salary: number
    age: number
    profileImage: string
    anualSalary: number
  }
  
  export interface TransactionResult {
    code: string
    description: string
  }

  export interface ResponseGetEmployees {
    employees: Employee[]
    transactionResult: TransactionResult
  }

  export interface ResponseGetEmployeeById {
    employee: Employee
    transactionResult: TransactionResult
  }