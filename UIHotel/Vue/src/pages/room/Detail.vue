<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title>
                <h2 class="card-title mb-0">Room Detail : Big No. 201</h2>
            </v-card-title>
            <div class="card-block">
                <div class="row">
                    <div class="col-2">
                        <img class="img-fluid" src="http://localhost.com/images/room.jpg" />
                    </div>
                    <div class="col-10">
                        <form>
                            <div class="form-group row">
                                <label class="col-2 col-form-label">Room Type</label>
                                <div class="col-4">
                                    <select class="form-control">
                                        <option>Room Type</option>
                                        <option selected>Big</option>
                                        <option>Medium</option>
                                        <option>Small</option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-2 col-form-label">Room Number</label>
                                <div class="col-4">
                                    <input class="form-control" placeholder="Room Number" value="201" type="text">
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-2 col-form-label">Room Status</label>
                                <div class="col-4">
                                    <input class="form-control" placeholder="" value="Vacant" type="text">
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="col-2 col-form-label">Floor Number</label>
                                <div class="col-4">
                                    <input class="form-control" placeholder="Floor" value="1" type="number">
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-10 offset-2">
                                    <div class="btn-group-lg">
                                        <a class="btn success" href="http://localhost.com/home/get/checkin"><i class="zmdi zmdi-download"></i> Checkin</a>
                                        <a class="btn info" href="http://localhost.com/home/get/booking"><i class="zmdi zmdi-bookmark"></i> Booking</a>
                                        <a class="btn error" href="http://localhost.com/home/get/checkout"><i class="zmdi zmdi-upload"></i> Checkout</a>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </v-card>
    </v-app>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                
            }
        }
    }
</script>
