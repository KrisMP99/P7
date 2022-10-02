import React from 'react';
import { Container } from 'react-bootstrap';
import '../Module.css';
import './ExerciseDescription.css';

interface ExerciseDescriptionProps {
    exerciseID: number;
    // user: User
}

export default function ExerciseDescriptionModule() {
    return (
        <div id='exercise-description-container' className='module-container'>
            <div>
                <h1>Exercise 1</h1>
                <p>
                    Lorem ipsum dolor, sit amet consectetur adipisicing elit. Ullam delectus neque cumque aliquid, 
                    obcaecati soluta debitis itaque excepturi error minima similique atque dignissimos quam, sit sed in nobis nam eum!
                </p>
            </div>
        </div>
    )
}

