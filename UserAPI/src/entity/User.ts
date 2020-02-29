import {Entity, BaseEntity, PrimaryGeneratedColumn, Column} from "typeorm";


@Entity("User")
export class User extends BaseEntity {
    
    @PrimaryGeneratedColumn()
    id: number;

    @Column()
    uid: string;

    @Column({unique: true})
    email: string;

    @Column({length: 128})
    password: string;

    @Column()
    isActive: boolean;

    @Column()
    firstName: string;

    @Column()
    lastName: string;

    @Column({length: 16})
    salt: string;

    @Column()
    isUserConfirmed: boolean;

    @Column("timestamp", {default: () => "CURRENT_TIMESTAMP"})
    createdAt: string;

}