import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import { User } from '../../App';
import ExerciseBoard from '../ExerciseBoard/ExerciseBoard';
import ExerciseView from '../ExerciseBoard/ExerciseView';

interface ExerciseViewHandlerProps {
    user: User;
}

export default function ExerciseViewHandler(props: ExerciseViewHandlerProps) {
    
    const [viewElement, setViewElement] = useState<JSX.Element>(<></>);

    const params = useParams();
    const exerciseId = !params.exerciseId ? undefined : Number(params.exerciseId);
    const isEdit = !params.isEdit ? undefined : Number(params.isEdit);
    const exerciseGroupId = !params.exerciseGroupId ? undefined : Number(params.exerciseGroupId);
    const courseId = !params.courseId ? undefined : Number(params.courseId);
    const navigate = useNavigate();

    useEffect(() => {
        if (!exerciseId || !exerciseGroupId || !courseId) {
            sessionStorage.getItem('jwt') ? navigate('/home') : navigate('/');
        }
        else if (exerciseId <= -1) {
            setViewElement(<ExerciseBoard exerciseId={exerciseId!} exerciseGroupId={exerciseGroupId} isNewExercise={true} courseId={courseId} />);
        }
        else if (exerciseId >= 0) {
            if (isEdit === 1) setViewElement(<ExerciseBoard exerciseId={exerciseId!} exerciseGroupId={exerciseGroupId} isNewExercise={false} courseId={courseId} />);
            else if (isEdit === 0) setViewElement(<ExerciseView exerciseId={exerciseId!} exerciseGroupId={exerciseGroupId} courseId={courseId} />);
        }
    }, [exerciseId, isEdit, exerciseGroupId]);

    return viewElement;
}
