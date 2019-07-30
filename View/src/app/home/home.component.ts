import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  show: number = 1;
  constructor(private router: Router) { }

  ngOnInit() {
  }

  tablePanel()
  {
    this.show = 1;
  }
 searchPanel ()
  {
    this.show = 2;
  }
  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

}
