import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Presents } from './presents';

describe('Presents', () => {
  let component: Presents;
  let fixture: ComponentFixture<Presents>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Presents]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Presents);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
