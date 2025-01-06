import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { DislikesService } from '../_services/dislikes.service';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FormsModule } from '@angular/forms';
import { MemberCardComponent } from "../members/member-card/member-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [ButtonsModule, FormsModule, MemberCardComponent, PaginationModule],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit, OnDestroy {
  dislikesService = inject(DislikesService);
  predicate = "disliked";
  pageNumber = 1;
  pageSize = 5;

  ngOnInit(): void {
    this.loadDislikes();
  }
  
  getTitle(){
    switch (this.predicate) {
      case 'disliked' : return "Members you disliked";
      case 'dislikedBy': return "Members who disliked you";
      default: return "Mutual disliked"
    }
  }

  loadDislikes() {
    this.dislikesService.getDislikes(this.predicate, this.pageNumber, this.pageSize);
  }

  pageChanged(event: any) {
    if(this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadDislikes();
    }
  }
  
  ngOnDestroy(): void {
    this.dislikesService.paginatedResult.set(null);
  }

}
