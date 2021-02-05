import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { ProductService } from 'src/app/core/services/product.service';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  product: any ;
  submited: boolean= false;
  id = this.activatedRoute.snapshot.params['id'];

  selectedfile: any ;

  productForm = new FormGroup({
    name: new FormControl('', [Validators.required,
    ]),
    price: new FormControl('', [Validators.required,
    ]),
    photoPath: new FormControl('', [Validators.required]),
  })

  constructor(
    private activatedRoute: ActivatedRoute,
    private productService: ProductService,
    private alertifyService: AlertifyService,
    private router: Router,
  ) {
    if (this.id != null) {
      this.getCategory(this.id);
    }
  }
  ngOnInit(): void {
  }
  getCategory(id: number) {
    this.productService.getProduct(id).subscribe((res) => {
      this.product = res;
      this.productForm.patchValue({
        name: this.product.name,
        price: this.product.price,
        photoPath: this.product.photoPath
      });
    }, error => {
      console.log("error")
    });
  }

  OnFileSelected(event: any) {
      const file = (event.target).files[0];
      var mimeType = file.type;
      if (mimeType.match(/image\/*/) == null) {
        //this.message = "Only images are supported.";
        return;
      }
      this.selectedfile = file;
  }

  onSubmit() {
    this.submited = true;
    if (this.productForm.valid) {
      this.product = this.productForm.value as Product;
      let formData = new FormData();
      formData.append("name", this.product.name);
      formData.append("price", this.product.price);
      if (this.selectedfile != null) {
        formData.append("Photo", this.selectedfile, this.selectedfile.name);
      }
      if (this.id == undefined) {
        this.productService.creatProduct(formData).subscribe((res) => {
          this.alertifyService.success("Done Create")
          this.router.navigate(['products'])
        }, error => {
          this.alertifyService.error("Error")
        });
      } else {
        formData.set('id', this.id.toString());
        this.productService.updateProduct(formData).subscribe((res: any) => {
          this.alertifyService.success("Done Update")

          this.router.navigate(['products'])
        }, error => {
          this.alertifyService.error("Error")
        });
      }

    }

  }



}
