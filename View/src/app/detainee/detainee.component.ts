import { Component, OnInit, Input } from '@angular/core';
import { Detainee } from '../models/detainee';
import { DetaineeService }  from '../services/detainee.service';
import { SmartDetention } from '../models/smartdetention';

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
  private detaineeService: DetaineeService
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
    //this.tableMode = true;
  }

  getDetainee(): void { 
    this.detaineeService.getDetainee(this.id)
    .subscribe(detainee => this.detainee = detainee);
    this.number='№';
  }
}
