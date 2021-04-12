import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPetitionComponent } from './show-petition.component';

describe('ShowPetitionComponent', () => {
  let component: ShowPetitionComponent;
  let fixture: ComponentFixture<ShowPetitionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowPetitionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowPetitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
