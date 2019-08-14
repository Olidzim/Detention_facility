import { Component, OnInit, Input } from '@angular/core';
import { Detainee } from '../models/detainee';
import { DetaineeService }  from '../services/detainee.service';
import { SmartDetention } from '../models/smartdetention';
import { SmartDetainee } from '../models/smartdetainee';
import { Router } from '@angular/router';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-detainee',
  templateUrl: './detainee.component.html',
  styleUrls: ['./detainee.component.css']
})
export class DetaineeComponent implements OnInit {
  @Input() detainee: Detainee;
  @Input() smartDetentions: SmartDetention[];

  detainees : Detainee[];
  number: string ='';
  id: number;
  tableMode: boolean = true;
  
  constructor(
  private detaineeService: DetaineeService,
  private router: Router,
  private sharedService: SharedService
  ) { }

  ngOnInit() {    
    this.loadsmartDetainees();  
  }


  loadsmartDetainees() { 
    this.detaineeService.getsmartDetainees()
    .subscribe((data: Detainee[]) => this.detainees = data);
  }


  editdetainee(d:Detainee) {
    this.detainee = d;
  }


  delete(p: Detainee) {
    this.detaineeService.deleteDetainee(p.detaineeID)
    .subscribe(data => this.loadsmartDetainees());
  }


  saveChanges()
  { 
    this.detaineeService.updateDetainee(this.detainee)
    .subscribe(data => this.loadsmartDetainees());
    this.cancel();    
  }


  cancel() {
    this.detainee = new Detainee();    
  }


  toDetaineeDetail(d: SmartDetainee)
  {    
    this.sharedService.forDetaineeDetailID = d.detaineeID;    
    this.router.navigateByUrl('/home/detainee/detainee-detail/'+d.detaineeID);
  }
  

  getDetainee(): void { 
    this.detaineeService.getDetainee(this.id)
    .subscribe(detainee => this.detainee = detainee);
    this.number='№';
  }
}
