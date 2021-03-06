﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarInput {

	// ---------------------------------------------------------------- //

	public float	throttle   = 0.0f; 
	public float	brake      = 0.0f; 
	public float	steer      = 0.0f;
	public float	hand_brake = 0.0f;

	public bool		is_reverse = false;			// バックギア中？.

	public float	gear_reverse_timer = 0.0f;

	protected CarControl	car_control;

	// ================================================================ //

	public CarInput(CarControl car_control)
	{
		this.car_control = car_control;
	}

	public void		execute()
	{
		if(Input.GetKeyDown(KeyCode.B)) {

			this.is_reverse = !this.is_reverse;
		}

		bool	is_accel_on = false;
		bool	is_brake_on = false;

		if(!this.is_reverse) {

			is_accel_on = Input.GetKey(KeyCode.UpArrow);
			is_brake_on = Input.GetKey(KeyCode.DownArrow);

		} else {

			// バックギアーのときは、上下キーを入れかえる.
			is_accel_on = Input.GetKey(KeyCode.DownArrow);
			is_brake_on = Input.GetKey(KeyCode.UpArrow);
		}

		// アクセル.
		if(is_accel_on) {

			this.throttle = 1.0f;

		} else {

			this.throttle = 0.0f;
		}

		// ブレーキ.
		if(is_brake_on) {

			this.brake = 1.0f;

		} else {

			this.brake = 0.0f;
		}

		// ステアリング.
		if(Input.GetKey(KeyCode.LeftArrow)) {

			this.steer = -1.0f;

		} else if(Input.GetKey(KeyCode.RightArrow)) {

			this.steer =  1.0f;

		} else {

			this.steer =  0.0f;
		}

		if(Input.GetKey(KeyCode.Keypad0)) {

			this.hand_brake = 1.0f;

		} else {

			this.hand_brake = 0.0f;
		}

		// 前進/後退　切り替え.

		float	stop_speed = 0.01f;

		if(Mathf.Abs(this.car_control.relative_velocity.z) < stop_speed && is_brake_on) {

			this.gear_reverse_timer += Time.deltaTime;

		} else {

			this.gear_reverse_timer = 0.0f;
		}

		if(this.gear_reverse_timer > 1.0f) {

			this.is_reverse = !this.is_reverse;
		}
	}
}